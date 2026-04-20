# Setup Progetto - AppBase (.NET 10 + Angular, architettura unica)

Questo documento descrive la sequenza completa di preparazione, esecuzione e pubblicazione del progetto.

## 0. Creazione e preparazione iniziale (come e perche)

Questa e la sequenza consigliata quando devi creare il progetto da zero con architettura unica.

### 0.1 Creazione struttura repository

1. Crea cartelle principali:
  - `backend/AppBase.Api`
  - `frontend`
2. Perche:
  - separi chiaramente sorgenti API e SPA
  - mantieni deploy unico, ma sviluppo ordinato

### 0.2 Creazione backend ASP.NET Core

Comandi tipici:
- `dotnet new webapi -n AppBase.Api -o backend/AppBase.Api`

Perche:
- template stabile e gia pronto per controller/API
- integrazione naturale con publish .NET

### 0.3 Creazione frontend Angular

Comandi tipici:
- `ng new frontend --routing --style=scss`

Perche:
- Angular fornisce SPA moderna con build production ottimizzata
- routing client-side gestibile con fallback su backend

### 0.4 Installazione dipendenze e primo allineamento

1. Backend:
  - `dotnet restore backend/AppBase.Api/AppBase.Api.csproj`
2. Frontend:
  - `cd frontend`
  - `npm install`

Perche:
- blocchi subito mismatch di versioni
- garantisci una baseline riproducibile su tutte le macchine

### 0.4.1 Aggiunta NGXS (state management)

Comando tipico:
- `cd frontend`
- `npm install @ngxs/store`

Perche NGXS in questo progetto:
- centralizza stato UI e stato asincrono API in un solo punto
- rende piu leggibile il flusso Action -> State -> Selector
- facilita evoluzione verso piu feature senza logica sparsa nei componenti

Configurazione minima da fare subito:
1. Registra lo store in `frontend/src/app/app.config.ts`:
  - `provideStore([CounterState])`
2. Crea uno state in `frontend/src/app/state/counter.state.ts` con:
  - model dello stato
  - defaults
  - action handlers
  - selectors
3. Usa lo state dal componente tramite `Store` e `select` (es. in `frontend/src/app/app.ts`).

### 0.5 Scelta architettura unica: decisione tecnica

Decisione:
- build Angular in `backend/AppBase.Api/wwwroot`
- backend serve API + file statici

Perche:
- un solo artefatto di deploy
- pipeline piu semplice in ambienti piccoli/medi
- riduzione complessita di reverse proxy tra due app separate

Trade-off da conoscere:
- ciclo frontend puro meno rapido rispetto a hosting separato
- ogni release backend porta anche asset frontend

### 0.6 Preparazione configurazioni base

1. Angular output path verso `wwwroot`.
2. Backend con `UseDefaultFiles`, `UseStaticFiles`, `MapFallbackToFile("index.html")`.
3. Swagger abilitato in Development.
4. SpaProxy per avvio automatico del frontend in Development.
5. Task VS Code per build/run rapidi del backend.

Perche:
- riduci errori manuali durante sviluppo e publish
- stessa esperienza locale e in ambiente di rilascio

### 0.7 Criterio di validazione iniziale

Dopo setup iniziale, verifica sempre:
1. `dotnet build backend/AppBase.Api/AppBase.Api.csproj`
2. `npm run build -- --configuration production` in `frontend`
3. `dotnet run --project backend/AppBase.Api/AppBase.Api.csproj`
4. apertura URL:
  - `https://localhost:7236/`
  - `https://localhost:7236/api/status`
  - `https://localhost:7236/swagger/index.html`

Perche:
- confermi che architettura unica, API e SPA sono davvero integrate
- intercetti subito problemi di certificato, path o fallback routing

## 1. Obiettivo architettura

Architettura unica:
- Backend ASP.NET Core serve API + file statici frontend.
- Frontend Angular viene compilato dentro `backend/AppBase.Api/wwwroot`.
- Publish del backend include automaticamente la build frontend.

## 2. Prerequisiti

Installare:
- .NET SDK 10
- Node.js + npm
- Angular CLI (opzionale, se usi `npx ng` non e obbligatorio globale)

Verifica rapida:
- `dotnet --version`
- `node -v`
- `npm -v`

## 3. Struttura rilevante

- `backend/AppBase.Api`
  - API ASP.NET Core
  - pubblicazione finale
- `frontend`
  - sorgente Angular
  - build configurata verso `../backend/AppBase.Api/wwwroot`
- `.vscode/launch.json`
  - debug backend
- `.vscode/tasks.json`
  - task build/run backend

## 4. Configurazioni importanti applicate

### 4.1 Frontend output in wwwroot

File: `frontend/angular.json`

Configurazione chiave:
- `projects.frontend.architect.build.options.outputPath.base = "../backend/AppBase.Api/wwwroot"`
- `projects.frontend.architect.build.options.outputPath.browser = ""`

Effetto:
- `ng build` produce direttamente `index.html`, JS e CSS in `backend/AppBase.Api/wwwroot`.

### 4.2 Backend serve SPA + API

File: `backend/AppBase.Api/Program.cs`

Pipeline chiave:
- Swagger in Development
- `UseHttpsRedirection()`
- `UseDefaultFiles()`
- `UseStaticFiles()`
- `MapControllers()`
- `MapFallbackToFile("index.html")`

Effetto:
- `/api/...` gestito dai controller
- rotte frontend Angular gestite da fallback su `index.html`

### 4.3 Swagger

File: `backend/AppBase.Api/AppBase.Api.csproj`

Pacchetti:
- `Microsoft.AspNetCore.OpenApi` 10.0.5
- `Swashbuckle.AspNetCore` 10.1.7

Nota compatibilita:
- Versioni 6.x di Swashbuckle con net10 causano `TypeLoadException`.
- Usare 10.1.7+ con net10.

### 4.4 Build frontend automatica al publish backend

File: `backend/AppBase.Api/AppBase.Api.csproj`

Target MSBuild:
- `BuildFrontendForPublish` con `BeforeTargets="Publish"`
- Esegue:
  - `npm install`
  - `npm run build -- --configuration production`

Effetto:
- `dotnet publish` prepara anche la SPA senza passaggi manuali aggiuntivi.

### 4.5 SpaProxy per sviluppo locale con F5

File: `backend/AppBase.Api/AppBase.Api.csproj`

Proprieta chiave:
- `SpaRoot = ..\\..\\frontend\\`
- `SpaProxyServerUrl = http://localhost:4200`
- `SpaProxyLaunchCommand = npm start`
- `SpaProxyWorkingDirectory = $(SpaRoot)`

Pacchetto:
- `Microsoft.AspNetCore.SpaProxy` 10.0.5

File: `backend/AppBase.Api/Properties/launchSettings.json`

Variabile ambiente in Development:
- `ASPNETCORE_HOSTINGSTARTUPASSEMBLIES = Microsoft.AspNetCore.SpaProxy`

File: `.vscode/launch.json`

Variabile ambiente aggiunta alla configurazione `Launch Backend`:
- `ASPNETCORE_HOSTINGSTARTUPASSEMBLIES = Microsoft.AspNetCore.SpaProxy`

Effetto:
- avviando il backend in debug (F5) il frontend Angular viene avviato automaticamente tramite `npm start`
- non e necessario avviare manualmente `ng serve` da task root

### 4.6 TS warning TS6 (rootDir)

File: `frontend/tsconfig.app.json`

Impostato:
- `"rootDir": "./src"`

Motivo:
- Rimuovere warning TS6 sul common source directory.

### 4.7 NGXS integrato nel progetto

File coinvolti:
- `frontend/package.json` -> dipendenza `@ngxs/store`
- `frontend/src/app/app.config.ts` -> `provideStore([CounterState])`
- `frontend/src/app/state/counter.state.ts` -> stato applicativo, action e selector

Uso attuale:
- gestione contatore
- chiamata API `/api/status` con loading ed error handling nello state

### 4.8 Esempio rapido NGXS (Action + Selector)

Esempio minimo da replicare quando aggiungi una nuova feature stato.

```ts
export class SetTitle {
  static readonly type = '[Ui] Set Title';
  constructor(public readonly title: string) {}
}

export interface UiStateModel {
  title: string;
}

@State<UiStateModel>({
  name: 'ui',
  defaults: {
    title: 'Dashboard',
  },
})
@Injectable()
export class UiState {
  @Selector()
  static title(state: UiStateModel): string {
    return state.title;
  }

  @Action(SetTitle)
  setTitle(ctx: StateContext<UiStateModel>, action: SetTitle): void {
    ctx.patchState({ title: action.title });
  }
}
```

Uso nel componente:
1. dispatch action:
   - `this.store.dispatch(new SetTitle('Ordini'));`
2. read selector:
   - `this.store.select(UiState.title)`

Perche questo pattern:
- action descrive cosa succede
- state centralizza aggiornamenti e side effects
- selector espone dati pronti alla UI

## 5. Sequenza operativa consigliata (giornaliera)

### 5.1 Prima inizializzazione

1. Apri workspace root `app_base`.
2. Ripristina backend:
   - `dotnet restore backend/AppBase.Api/AppBase.Api.csproj`
3. Installa frontend:
   - `cd frontend`
   - `npm install`

### 5.2 Sviluppo API + test rapido

Da root:
- `dotnet build backend/AppBase.Api/AppBase.Api.csproj`
- `dotnet run --project backend/AppBase.Api/AppBase.Api.csproj`

Test endpoint:
- `https://localhost:7236/api/status`
- Swagger UI: `https://localhost:7236/swagger/index.html`

### 5.3 Sviluppo frontend separato (opzionale)

Per sviluppo veloce con hot reload:
- `cd frontend`
- `ng serve`

Proxy API:
- `frontend/proxy.conf.json` punta a `https://localhost:7236`.

### 5.4 Build frontend verso backend (quando vuoi test integrato)

Da `frontend`:
- `npm run build -- --configuration production`

Poi avvia backend e testa su:
- `https://localhost:7236/`

## 6. Publish produzione

Comando:
- `dotnet publish backend/AppBase.Api/AppBase.Api.csproj -c Release`

Cosa succede:
1. Restore backend
2. Esecuzione target `BuildFrontendForPublish`
3. Build Angular production in `wwwroot`
4. Publish backend completo in `backend/AppBase.Api/bin/Release/net10.0/publish`

## 7. Debug da VS Code

### 7.1 Launch backend

File: `.vscode/launch.json`
- Config attiva: `Launch Backend`
- `preLaunchTask`: `build-backend`
- URL: `https://localhost:7236;http://localhost:5000`

### 7.2 Task utili

File: `.vscode/tasks.json`
- `build-backend`
- `run-backend`
- `start-angular`

## 8. Problemi comuni e soluzione

### 8.1 `dotnet run` da root fallisce

Causa:
- da root non c'e un progetto eseguibile selezionato.

Soluzione:
- usare sempre:
  - `dotnet run --project backend/AppBase.Api/AppBase.Api.csproj`

### 8.2 Messaggi debugger "Loaded ... Skipped loading symbols"

Significato:
- messaggi informativi, non errori runtime.

Stato:
- ridotti con `moduleLoad: false` in `.vscode/launch.json`.

### 8.3 Certificato dev HTTPS non trusted

Messaggio tipico:
- warning ASP.NET Core developer certificate not trusted.

Impatto:
- in locale puo non bloccare (specie con proxy `secure: false`), ma e consigliato sistemarlo.

Procedura consigliata (PowerShell):
1. Posizionati nella root del workspace.
2. Esegui in sequenza:
  - `dotnet dev-certs https --clean`
  - `dotnet dev-certs https --trust`
  - `dotnet dev-certs https --check --trust`

Perche questi tre comandi:
- `--clean`: rimuove certificati locali vecchi/corrotti.
- `--trust`: crea e aggiunge attendibilita al nuovo certificato localhost.
- `--check --trust`: verifica finale che un certificato trusted esista davvero.

Validazione dopo fix:
1. Avvia API:
  - `dotnet run --project backend/AppBase.Api/AppBase.Api.csproj`
2. Controlla che non compaia piu il warning sul certificato non trusted.
3. Apri in browser:
  - `https://localhost:7236/swagger/index.html`

Nota Windows:
- durante `--trust` puo comparire un prompt di conferma del sistema: accetta per completare l'operazione.

## 9. Checklist veloce prima di consegna

1. `dotnet build backend/AppBase.Api/AppBase.Api.csproj`
2. `npm run build -- --configuration production` in `frontend`
3. `dotnet publish backend/AppBase.Api/AppBase.Api.csproj -c Release`
4. Verifica:
   - `https://localhost:7236/`
   - `https://localhost:7236/api/status`
   - `https://localhost:7236/swagger/index.html`

## 10. Note operative finali

- Se vuoi solo API in debug: avvia backend direttamente.
- Se vuoi UX frontend rapida durante coding: usa `ng serve`.
- Per test realistico pre-release: usa frontend buildato in `wwwroot` servito dal backend.
