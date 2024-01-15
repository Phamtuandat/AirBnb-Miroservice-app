import { Params } from '@angular/router';

export type DataResponse<T> = {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: T[];
};

export type Pagination = {
  pageIndex: number;
  pageSize: number;
  count: number;
};

export type Label = {
  id: string;
  imgUrl: string;
  name: string;
};

export type QueryParams = Params & {
  labelId: string | undefined;
};
