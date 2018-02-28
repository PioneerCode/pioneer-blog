import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Tag } from '../models/tag';
import 'rxjs/add/operator/toPromise';
import { environment } from '../../environments/environment';

@Injectable()
export class TagRepository {
  private url = environment.apiUrl + '/api/tags';

  constructor(private http: HttpClient) { }

  get(id: number, includeExcerpt = false): Promise<Tag> {
    return this.http.get(this.url + '/' + id)
      .toPromise()
      .then((res: Response) => {
        const body: Tag = res.json();
        return body || {} as Tag;
      })
      .catch(this.handleError);
  }

  getAll(): Promise<Tag[]> {
    return this.http.get(this.url)
      .toPromise()
      .then((res: Response) => {
        const body: Tag[] = res.json();
        return body || [];
      })
      .catch(this.handleError);
  }

  create(): Promise<Tag> {
    return this.http.post(this.url, {} as Tag)
      .toPromise()
      .then((res: Response) => {
        const body: Tag = res.json();
        return body || {} as Tag;
      })
      .catch(this.handleError);
  }

  save(tag: Tag): Promise<Response> {
    return this.http.put(this.url + '\\' + tag.tagId, tag)
      .toPromise()
      .catch(this.handleError);
  }

  remove(id: number): Promise<Response> {
    return this.http.delete(this.url + '\\' + id)
      .toPromise()
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred (Tag Repository)', error);
    return Promise.reject(error.message || error);
  }
}
