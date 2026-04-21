import { provideHttpClient } from '@angular/common/http';
import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideStore } from '@ngxs/store';
import { routes } from './app.routes';
import { CounterState } from './state/counter.state';
import { Sidebar } from './components/sidebar/sidebar';

//components
import { Home } from './components/home/home';

import { Layout } from './components/layout/layout';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHttpClient(),
    provideStore([CounterState]),
    Home,
    Sidebar,
    Layout,
  ],
};
