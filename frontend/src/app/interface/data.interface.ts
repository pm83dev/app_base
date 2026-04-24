export interface DataItem {
  id: number;
  name: string;
  value: string;
}

export interface DataResponseModel {
  data: DataItem[]; //Array of DataItem objects returned from the API
  loading?: boolean;
  error?: string;
}
