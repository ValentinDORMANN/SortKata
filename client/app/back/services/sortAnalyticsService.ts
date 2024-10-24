import { ESortType } from '@/app/models/eSortType';
import { SortAnalytics } from '@/app/models/sortAnalytics';

export class SortAnalyticsService {
  private static readonly _BASE_URL = 'http://localhost:5135';

  public static async getSortAnalytics(items: number[], sortType: ESortType): Promise<SortAnalytics> {
    const body = {
      list: items,
      sortType: sortType.toString(),
    };
    const url: string = `${SortAnalyticsService._BASE_URL}/sort/numeric/analytics`;

    try {
      const response = await fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*',
          'Access-Control-Allow-Headers': '*'
        },
        body: JSON.stringify(body),
      });

      if (!response.ok) {
        throw new Error(`Error when calling API: ${response.status}`);
      }

      const data = await response.json();
      return data;
    } catch (error) {
      console.error('Error:', error);
      throw error;
    }
  }
}
