import { Injectable } from '@angular/core';

@Injectable()
export class PaginationService {

    constructor() { }

    getPages(_totalPages: number, currentPage: number = 1) {
        const totalPages = _totalPages;
        let startPage: number, endPage: number;
        if (totalPages <= 10) {
            startPage = 1;
            endPage = totalPages;
        } else {
            if (currentPage <= 6) {
                startPage = 1;
                endPage = 10;
            } else if (currentPage + 4 >= totalPages) {
                startPage = totalPages - 9;
                endPage = totalPages;
            } else {
                startPage = currentPage - 5;
                endPage = currentPage + 4;
            }
        }
        const pages = Array.from(Array(endPage + 1).keys());
        pages.splice(0, startPage);
        return pages;
    }
}

export class PagedData<T> {
    totalRows: number;
    page: number;
    pageSize: number;
    order: string;
    search: string;
    pages: Array<number>;
    totalPages: number;
    rows: Array<T>;
    isAscending: boolean;

    constructor() {
        this.rows = new Array<T>();
    }
}
