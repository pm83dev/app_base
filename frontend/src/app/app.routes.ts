import { Routes } from '@angular/router';
import { Layout } from './components/layout/layout';
import { Home } from './components/pages/home/home';
import { Data } from './components/pages/data/data';

export const routes: Routes = [
  {
    path: '',
    component: Layout,
    children: [
      { path: '', component: Home },
      { path: 'data', component: Data },
      // altre pagine qui
    ],
  },
];
