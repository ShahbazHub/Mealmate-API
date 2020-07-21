export class QueryParamsModel {
  // fields
  filter: any;
  sortOrder: string; // asc || desc
  sortField: string;
  pageIndex: number;
  pageSize: number;
  pagingStrategy: string;

  // constructor overrides
  constructor(
    filter,
    sortOrder = "asc",
    sortField = "",
    pageNumber = 0,
    pageSize = 10
  ) {
    this.filter = filter;
    this.sortOrder = sortOrder;
    this.sortField = sortField;
    this.pageIndex = pageNumber;
    this.pageSize = pageSize;
    this.pagingStrategy = "withCount";
  }
}
