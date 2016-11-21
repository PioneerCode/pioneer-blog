import { Pipe } from '@angular/core';

@Pipe({
  name: 'truncate'
})
export class TruncatePipe {
  transform(value: string, args: number): string {
    let limit = args ? args : 10;
    return value.length > limit ? value.substring(0, limit) + '...' : value;
  }
}