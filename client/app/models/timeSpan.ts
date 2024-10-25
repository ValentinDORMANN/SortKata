import { Toolbox } from '../utils/toolbox';

export class TimeSpanOverflowError extends Error {
  public constructor(msg?: string) {
    super(Toolbox.isNullOrWhiteSpace(msg) ? msg : 'TimeSpanTooLong');
    Object.setPrototypeOf(this, TimeSpanOverflowError.prototype);
  }
}

export class TimeSpan {
  private static readonly _MILLIS_PER_SECOND = 1000;
  private static readonly _MILLIS_PER_MINUTE = TimeSpan._MILLIS_PER_SECOND * 60;   //     60,000
  private static readonly _MILLIS_PER_HOUR = TimeSpan._MILLIS_PER_MINUTE * 60;     //  3,600,000
  private static readonly _MILLIS_PER_DAY = TimeSpan._MILLIS_PER_HOUR * 24;        // 86,400,000
  
  private _millis: number;

  public constructor(millis?: number) {
    this._millis = millis ?? 0;
  }

  private static interval(value: number, scale: number): TimeSpan {
    if (Number.isNaN(value)) {
      throw new Error('value cannot be NaN');
    }

    const tmp = value * scale;
    const millis = TimeSpan.round(tmp + (value >= 0 ? 0.5 : -0.5));
    if ((millis > TimeSpan.maxValue.totalMilliseconds) || (millis < TimeSpan.minValue.totalMilliseconds)) {
      throw new TimeSpanOverflowError('TimeSpanTooLong');
    }

    return new TimeSpan(millis);
  }

  private static round(n: number): number {
    if (n < 0) {
      return Math.ceil(n);
    } else if (n > 0) {
      return Math.floor(n);
    }
    return 0;
  }

  private static timeToMilliseconds(hour: number, minute: number, second: number): number {
    const totalSeconds = (hour * 3600) + (minute * 60) + second;
    if (totalSeconds > TimeSpan.maxValue.totalSeconds || totalSeconds < TimeSpan.minValue.totalSeconds) {
      throw new TimeSpanOverflowError('TimeSpanTooLong');
    }

    return totalSeconds * TimeSpan._MILLIS_PER_SECOND;
  }

  public static get zero(): TimeSpan {
    return new TimeSpan(0);
  }

  public static get maxValue(): TimeSpan {
    return new TimeSpan(Number.MAX_SAFE_INTEGER);
  }

  public static get minValue(): TimeSpan {
    return new TimeSpan(Number.MIN_SAFE_INTEGER);
  }

  public static fromDays(value: number): TimeSpan {
    return TimeSpan.interval(value, TimeSpan._MILLIS_PER_DAY);
  }

  public static fromHours(value: number): TimeSpan {
    return TimeSpan.interval(value, TimeSpan._MILLIS_PER_HOUR);
  }

  public static fromMilliseconds(value: number): TimeSpan {
      return TimeSpan.interval(value, 1);
  }

  public static fromMinutes(value: number): TimeSpan {
    return TimeSpan.interval(value, TimeSpan._MILLIS_PER_MINUTE);
  }

  public static fromSeconds(value: number): TimeSpan {
    return TimeSpan.interval(value, TimeSpan._MILLIS_PER_SECOND);
  }

  public static fromTime(hours: number, minutes: number, seconds: number): TimeSpan;
  public static fromTime(days: number, hours: number, minutes: number, seconds: number, milliseconds: number): TimeSpan;
  public static fromTime(daysOrHours: number, hoursOrMinutes: number, minutesOrSeconds: number, seconds?: number, milliseconds?: number): TimeSpan {
    if (seconds != undefined && milliseconds != undefined) {
      return this.fromTimeStartingFromDays(daysOrHours, hoursOrMinutes, minutesOrSeconds, seconds, milliseconds);
    } else {
      return this.fromTimeStartingFromHours(daysOrHours, hoursOrMinutes, minutesOrSeconds);
    }
  }

  private static fromTimeStartingFromHours(hours: number, minutes: number, seconds: number): TimeSpan {
    const millis = TimeSpan.timeToMilliseconds(hours, minutes, seconds);
    return new TimeSpan(millis);
  }

  private static fromTimeStartingFromDays(days: number, hours: number, minutes: number, seconds: number, milliseconds: number): TimeSpan {
    const totalMilliSeconds = (days * TimeSpan._MILLIS_PER_DAY)
      + (hours * TimeSpan._MILLIS_PER_HOUR)
      + (minutes * TimeSpan._MILLIS_PER_MINUTE)
      + (seconds * TimeSpan._MILLIS_PER_SECOND)
      + milliseconds;

    if (totalMilliSeconds > TimeSpan.maxValue.totalMilliseconds || totalMilliSeconds < TimeSpan.minValue.totalMilliseconds) {
      throw new TimeSpanOverflowError('TimeSpanTooLong');
    }
    return new TimeSpan(totalMilliSeconds);
  }

  public get days(): number {
    return TimeSpan.round(this._millis / TimeSpan._MILLIS_PER_DAY);
  }

  public get hours(): number {
    return TimeSpan.round((this._millis / TimeSpan._MILLIS_PER_HOUR) % 24);
  }

  public get minutes(): number {
    return TimeSpan.round((this._millis / TimeSpan._MILLIS_PER_MINUTE) % 60);
  }

  public get seconds(): number {
    return TimeSpan.round((this._millis / TimeSpan._MILLIS_PER_SECOND) % 60);
  }

  public get milliseconds(): number {
    return TimeSpan.round(this._millis % 1000);
  }

  public get totalDays(): number {
    return this._millis / TimeSpan._MILLIS_PER_DAY;
  }

  public get totalHours(): number {
    return this._millis / TimeSpan._MILLIS_PER_HOUR;
  }

  public get totalMinutes(): number {
    return this._millis / TimeSpan._MILLIS_PER_MINUTE;
  }

  public get totalSeconds(): number {
    return this._millis / TimeSpan._MILLIS_PER_SECOND;
  }

  public get totalMilliseconds(): number {
    return this._millis;
  }

  public add(ts: TimeSpan): TimeSpan {
    const result = this._millis + ts.totalMilliseconds;
    return new TimeSpan(result);
  }

  public subtract(ts: TimeSpan): TimeSpan {
    const result = this._millis - ts.totalMilliseconds;
    return new TimeSpan(result);
  }
}