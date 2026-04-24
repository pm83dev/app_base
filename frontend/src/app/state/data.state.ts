import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { catchError, tap, throwError } from 'rxjs';
import { DataItem, DataResponseModel } from '../interface/data.interface';
import { ApiService } from '../services/api.service';

//Actions static type definitions

export class LoadApiData {
  static readonly type = '[Data] Load Api Data';
}

export class AddDataItem {
  static readonly type = '[Data] Add Data Item';
  constructor(public payload: DataItem) {}
}

export class UpdateDataItem {
  static readonly type = '[Data] Update Data Item';
  constructor(public payload: DataItem) {}
}

export class DeleteDataItem {
  static readonly type = '[Data] Delete Data Item';
  constructor(public payload: number) {} // payload is the ID of the item to delete
}

//State decorator with default values
@State<DataResponseModel>({
  name: 'data',
  defaults: {
    data: [], // array vuoto, non un oggetto singolo
    loading: false,
    error: '',
  },
})
//State Class with dependency injection and methods for actions and selectors
@Injectable()
export class DataState {
  constructor(private readonly apiService: ApiService) {}

  //Selectors to access specific parts of the state
  @Selector()
  static data(state: DataResponseModel): DataItem[] {
    return state.data;
  }

  @Selector()
  static id(state: DataItem): number {
    return state.id;
  }
  @Selector()
  static name(state: DataItem): string {
    return state.name;
  }
  @Selector()
  static value(state: DataItem): string {
    return state.value;
  }

  //Actions to modify the state based on dispatched actions
  @Action(LoadApiData)
  loadApiData(ctx: StateContext<DataResponseModel>) {
    ctx.patchState({
      loading: true,
      error: '',
    });

    return this.apiService.getData().pipe(
      tap((response) => {
        ctx.patchState({
          data: response.data,
          loading: false,
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
