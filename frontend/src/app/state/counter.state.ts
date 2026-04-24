import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { catchError, tap, throwError } from 'rxjs';
import { ApiService } from '../services/api.service';
import { CounterStateModel } from '../interface/counter.interface';

export class IncrementCounter {
  static readonly type = '[Counter] Increment';
}

export class LoadApiStatus {
  static readonly type = '[Counter] Load Api Status';
}


@State<CounterStateModel>({
  name: 'counter',
  defaults: {
    count: 0,
    apiMessage: '',
    serverTime: '',
    loading: false,
    error: '',
  },
})
@Injectable()
export class CounterState {
  constructor(private readonly apiService: ApiService) {}

  @Selector()
  static count(state: CounterStateModel): number {
    return state.count;
  }

  @Selector()
  static apiMessage(state: CounterStateModel): string {
    return state.apiMessage;
  }

  @Selector()
  static serverTime(state: CounterStateModel): string {
    return state.serverTime;
  }

  @Selector()
  static loading(state: CounterStateModel): boolean {
    return state.loading;
  }

  @Selector()
  static error(state: CounterStateModel): string {
    return state.error;
  }

  @Action(IncrementCounter)
  increment(ctx: StateContext<CounterStateModel>): void {
    const state = ctx.getState();
    ctx.setState({
      ...state,
      count: state.count + 1,
    });
  }

  @Action(LoadApiStatus)
  loadApiStatus(ctx: StateContext<CounterStateModel>) {
    ctx.patchState({
      loading: true,
      error: '',
    });

    return this.apiService.getStatus().pipe(
      tap((response) => {
        ctx.patchState({
          apiMessage: response.message,
          serverTime: response.serverTime,
          loading: false,
          error: '',
        });
      }),
      catchError((error: unknown) => {
        ctx.patchState({
          loading: false,
          error: 'Impossibile contattare la API .NET. Avvia il backend su https://localhost:7236.',
        });

        return throwError(() => error);
      }),
    );
  }
}
