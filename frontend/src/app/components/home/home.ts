import { Component, inject } from '@angular/core';
import { Store } from '@ngxs/store';
import { CounterState, IncrementCounter, LoadApiStatus } from '../../state/counter.state';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [AsyncPipe],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
   private readonly store = inject(Store);

  protected readonly count$ = this.store.select(CounterState.count);
  protected readonly apiMessage$ = this.store.select(CounterState.apiMessage);
  protected readonly serverTime$ = this.store.select(CounterState.serverTime);
  protected readonly loading$ = this.store.select(CounterState.loading);
  protected readonly error$ = this.store.select(CounterState.error);

  protected increment(): void {
    this.store.dispatch(new IncrementCounter());
  }

  protected loadStatus(): void {
    this.store.dispatch(new LoadApiStatus());
  }
}
