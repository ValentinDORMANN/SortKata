import { Toolbox } from './toolbox';

describe('Toolbox', () => {
  describe('toTitleCase', () => {
    test.each([null, undefined])('should throws an error given a null or undefined string', (value: string | null | undefined) => {
      expect(() => { Toolbox.toTitleCase(value)}).toThrow(/Cannot read properties.*/);
    });

    test.each([
      ['', ''],
      ['my_string', 'My_string'],
      ['my-string', 'My-string'],
      ['my string', 'My String'],
      ['mY StRiNg', 'My String']
    ])('should return transformed string', (value, expected) => {
      const output: string = Toolbox.toTitleCase(value);
      
      expect(output).toEqual(expected)
    });
  });

  describe('isNullOrWhiteSpace', () => {
    test.each([null, undefined, '', ' '])('should returns true given null, undefined, empty or whitespace string', (value: string | null | undefined) => {
      const output: boolean = Toolbox.isNullOrWhiteSpace(value);

      expect(output).toBe(true);
    });

    const VALUE: string = 'I\'m a string';
    test('should returns false given other value', () => {
      const output: boolean = Toolbox.isNullOrWhiteSpace(VALUE);

      expect(output).toBe(false);
    })
  });

  describe('parseEnum', () => {
    enum EGender { MALE, FEMALE }
    enum EGenderStr { MALE = 'man' as any, FEMALE = 'woman' as any }
    
    test.each([undefined, null])('should return undefined given null or undefined value', (value: string | null | undefined) => {
      const output = Toolbox.parseEnum(value, EGender);

      expect(output).toBeUndefined();
    });
    test('should returns undefined given an unknown enum value', () => {
      const output = Toolbox.parseEnum('unknown', EGenderStr);

      expect(output).toBeUndefined();
    });
    test.each([
      ['man', 'MALE'], ['MALE', EGenderStr.MALE.toString()],
      ['woman', 'FEMALE'], ['FEMALE', EGenderStr.FEMALE.toString()]
    ])('should return parsed enum given valid key', (value, expected) => {
      const output = Toolbox.parseEnum(value, EGenderStr);

      expect(output).toEqual(expected);
    });
    test.each([
      ['Male', EGender.MALE],
      ['fEmAlE', EGender.FEMALE]
    ])('should return parsed enum given valid key case insensitive', (value, expected) => {
      const output = Toolbox.parseEnum(value, EGender);

      expect(output).toEqual(expected);
    });
  });


  describe('randomIntInclusive', () => {
    const MIN: number = 10;
    const MAX: number = 50;

    test('should return number between min and max', () => {
      for (let i = 0; i < 100; i++) {
        const output: number = Toolbox.randomIntInclusive(MIN, MAX);

        expect(output).toBeGreaterThanOrEqual(MIN);
        expect(output).toBeLessThanOrEqual(MAX);
      }
    });
  });

  describe('flatten', () => {
    test.each([undefined, null, {}])('should return empty object given undefined, null, or empty object', (object: object | null | undefined) => {
      const output: { [key: string]: any } = Toolbox.flatten(object);

      expect(output).toEqual({})
    });

    test('should returns empty object given Date', () => {
      const output: { [key: string]: any } = Toolbox.flatten(new Date());

      expect(output).toEqual({})
    });
    test('should returns flatten object', () => {
      const date = new Date();
      const object = {
        'key1': 1,
        'key2': {
          'key21': 'value21',
          'key22': true
        },
        'key3': {
          'key31': [0,1,2,3],
          'key32': {
            'key321': date
          }
        }
      }

      const output: { [key: string]: any } = Toolbox.flatten(object);

      expect(output).toBeDefined();
      expect(Object.keys(output).length).toEqual(5);
      expect(output['key1']).toEqual(1);
      expect(output['key2.key21']).toEqual('value21');
      expect(output['key2.key22']).toBe(true);
      expect(output['key3.key31']).toEqual([0,1,2,3]);
      expect(output['key3.key32.key321']).toEqual(date);
    });

    test.each(['.', '-', '::'])('should uses separator', (separator: string) => {
      const object = {
        'key1': 'value1',
        'key2': {
          'key21': 'value21',
          'key22': {
            'key221': 'value221'
          }
        }
      };

      const output: { [key: string]: any } = Toolbox.flatten(object, separator);

      expect(output).toBeDefined();
      expect(Object.keys(output).length).toEqual(3);
      expect(Object.keys(output)).toContain(`key1`);
      expect(Object.keys(output)).toContain(`key2${separator}key21`);
      expect(Object.keys(output)).toContain(`key2${separator}key22${separator}key221`);
    });
  });
});
