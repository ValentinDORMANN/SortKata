import { ESortType } from "./eSortType";

export class SortAnalytics {
  public sortType: string;
  public itemCount: number;
  public executionTime: number;
  public bestTimeComplexity: string;
  public worstTimeComplexity: string;
  public averageTimeComplexity: string;

  public constructor(
    sortType?: string,
    itemCount?: number, executionTime?: number,
    bestTimeComplexity?: string, worstTimeComplexity?: string, averageTimeComplexity?: string
  ) {
    this.sortType = sortType ?? ESortType.UNDEFINED.toString();
    this.itemCount = itemCount ?? 0;
    this.executionTime = executionTime ?? 0;
    this.bestTimeComplexity = bestTimeComplexity ?? 'NA';
    this.worstTimeComplexity = worstTimeComplexity ?? 'NA';
    this.averageTimeComplexity = averageTimeComplexity ?? 'NA';
  }
}
