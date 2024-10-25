/**
 * A utility class providing various static methods for string, number, and object manipulation.
 */
export abstract class Toolbox {
  /**
   * Converts a string to title case.
   * @param {string} str - The input string to convert.
   * @returns {string} - The string converted to title case.
   */
  public static toTitleCase(str: string): string {
    return str.replace(/\w\S*/g, (txt: string) => {
      return txt.charAt(0).toUpperCase() + txt.substring(1).toLowerCase();
    });
  }

  /**
   * Checks if a string is null, undefined, or consists only of whitespace.
   * @param {string} str - The input string to check.
   * @returns {boolean} - `true` if the string is null, undefined, or whitespace, otherwise `false`.
   */
  public static isNullOrWhiteSpace(str: string | null | undefined): boolean {
    return !str || str.trim().length === 0;
  }

  /**
   * Convert string to enum
   * @template T
   * @param {string} value - The string to convert.
   * @param {T} enumType - The enum type to convert to.
   * @returns {(T[keyof T] | undefined)} - The corresponding enum value, or `undefined` if not found.
   * @see [ref] {@link https://stackoverflow.com/questions/17380845/how-do-i-convert-a-string-to-enum-in-typescript}
   * @see [ref] {@link https://stackoverflow.com/questions/44883072/reverse-mapping-for-string-enums}
   * @remarks enum string value must be preceded by <any>
   */
  public static parseEnum<T>(value: string | null | undefined, enumType: T): T[keyof T] | undefined {
    if (!value) { return undefined; }
    for (const property in enumType) {
      const enumMember = enumType[property];
      if (typeof enumMember === 'string') {
        if (enumMember.toUpperCase() === value.toUpperCase()) {
          const key = enumMember as string as keyof typeof enumType;
          return enumType[key];
        }
      }
    }
    return undefined;
  }

  /**
   * Generates a random integer within the specified inclusive range.
   * @param {number} min - The minimum value.
   * @param {number} max - The maximum value.
   * @returns {number} - The random integer.
   */
  public static randomIntInclusive(min: number, max: number): number {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }

  /**
   * Flattens a nested object into a single depth object.
   * @param {any} obj - The object to flatten.
   * @param {string} [sep='.'] - The separator to use for nested keys.
   * @returns {{ [key: string]: any }} - The flattened object.
   * @see [ref] {@link https://stackoverflow.com/questions/33036487/one-liner-to-flatten-nested-object}
   * @see [ref] {@link https://www.geeksforgeeks.org/flatten-javascript-objects-into-a-single-depth-object/}
   */
  public static flatten(obj: any, sep: string = '.'): { [key: string]: any } {
    const result: { [key: string]: any } = {};
    for (const i in obj) {
      if ((typeof obj[i]) === 'object'
        && !Array.isArray(obj[i]) 
        && !(obj[i] instanceof Date)
      ) {
        const temp = Toolbox.flatten(obj[i], sep);
        for (const j in temp) {
          result[`${i}${sep}${j}`] = temp[j];
        }
      } else {
        result[i] = obj[i];
      }
    }
    return result;
  }
}