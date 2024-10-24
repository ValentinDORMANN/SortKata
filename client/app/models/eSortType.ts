export enum ESortType {
  UNDEFINED = 'undefined' as any,
  BUBBLE_SORT = 'bubble_sort' as any,
  COCKTAIL_SORT = 'cocktail_sort' as any,
  INSERTION_SORT = 'insertion_sort' as any,
  MERGE_BOTTOM_UP_SORT = 'merge_bottom_up_sort' as any,
  MERGE_TOP_DOWN_SORT = 'merge_top_down_sort' as any,
  QUICK_SORT = 'quick_sort' as any
}

export const SORT_TYPES: ESortType[] = [
  ESortType.BUBBLE_SORT,
  ESortType.COCKTAIL_SORT,
  ESortType.INSERTION_SORT,
  ESortType.MERGE_BOTTOM_UP_SORT,
  ESortType.MERGE_TOP_DOWN_SORT,
  ESortType.QUICK_SORT
];
