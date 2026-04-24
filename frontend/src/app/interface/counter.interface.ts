//State Model interface
export interface CounterStateModel {
  count: number;
  apiMessage: string;
  serverTime: string;
  loading: boolean;
  error: string;
}
