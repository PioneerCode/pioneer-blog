import { Injectable }               from '@angular/core';
import { Headers, Http, Response }  from '@angular/http';
import { Category }                 from '../../models/category';
import {Observable}                 from 'rxjs/Rx';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class CategoryRepository {
  private url = '/api/categories';

  constructor(private http: Http) { }

  get(id: number, includeExcerpt = false): Promise<Category> {
    return this.http.get(this.url + '/' + id)
      .toPromise()
      .then(res => {
        const body = res.json();
        return body || {} as Category;
      })
      .catch(this.handleError);
  }

  getAll(): Promise<Category[]> {
    return this.http.get(this.url)
      .toPromise()
      .then(res => {
        const body = res.json();
        return body || [];
      })
      .catch(this.handleError);
  }

  create(): Promise<Category> {
    return this.http.post(this.url, {} as Category)
      .toPromise()
      .then(res => {
        const body = res.json();
        return body || [];
      })
      .catch(this.handleError);
  }

  save(category: Category): Promise<Response> {
    return this.http.put(this.url + '\\' + category.categoryId, category)
      .toPromise()
      .catch(this.handleError);
  }

  remove(id: number): Promise<Response> {
    return this.http.delete(this.url + '\\' + id)
      .toPromise()
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
}