import { Component, EventEmitter, Input, Output } from '@angular/core';

export interface IPager {
  totalItems: number;
  currentPage: number;
  pageSize: number;
  totalPages: number;
  startPage: number;
  endPage: number;
  startIndex: number;
  endIndex: number;
  pages: number[];
}

@Component({
  selector: 'pc-pager',
  template: `
  <section class="pc-pager">
    <ul class="pagination" role="navigation" aria-label="Pagination">
      <li class="pagination-previous disabled"></li>
      <li *ngFor="let page of getPager().pages">
        <a (click)="onClick(page)">{{page}}</a>
      </li>
      <li class="pagination-next"><a href="#" aria-label="Next page"></a></li>
    </ul>
  </section>
  `
})

export class PagerComponent {
  @Input() page = 1;
  @Input() count = 1;

  @Output() onPageClicked = new EventEmitter<IPager>();

  pager = {} as IPager;

  ngOnInit(): void {
    this.setPager(this.page, this.count);
  }

  /**
   * Omit an event when a page is clicked
   */
  onClick(page: IPager) {
    this.onPageClicked.emit(page);
    this.setPager(page.currentPage, this.count);
  }

  getPager() {
    return this.pager;
  }

  setPager(page: number, totalItems: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    this.pager = this.buildPagerObject(totalItems, page);
  }

  private buildPagerObject(totalItems: number, currentPage: number) {
    // default to first page
    currentPage = currentPage || 1;

    // default page size is 4
    const pageSize = 4;

    // calculate total pages
    const totalPages = Math.ceil(totalItems / this.count);

    let startPage: number;
    let endPage: number;

    if (totalPages <= 4) {
      // less than 4 total pages so show all
      startPage = 1;
      endPage = totalPages;
    } else {
      // more than 4 total pages so calculate start and end pages
      if (currentPage <= 3) {
        startPage = 1;
        endPage = 4;
      } else if (currentPage + 1 >= totalPages) {
        startPage = totalPages - 3;
        endPage = totalPages;
      } else {
        startPage = currentPage - 2;
        endPage = currentPage + 1;
      }
    }

    // calculate start and end item indexes
    const startIndex = (currentPage - 1) * pageSize;
    const endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);

    // create an array of pages *ngFor
    const pages = this.range(startPage, endPage + 1);

    return {
      totalItems: totalItems,
      currentPage: currentPage,
      pageSize: pageSize,
      totalPages: totalPages,
      startPage: startPage,
      endPage: endPage,
      startIndex: startIndex,
      endIndex: endIndex,
      pages: pages
    } as IPager;
  }

  /**
   * Generate an integer Array containing an arithmetic progression.
   * Inspired by _.range()
   */
  private range(start: number, stop: number, step?: number) {
    if (stop == null) {
      stop = start || 0;
      start = 0;
    }
    if (!step) {
      step = stop < start ? -1 : 1;
    }

    const length = Math.max(Math.ceil((stop - start) / step), 0);
    const range = Array(length);

    for (let i = 0; i < length; i++ , start += step) {
      range[i] = start;
    }

    return range;
  }
}