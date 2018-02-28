import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Tag } from '../models/tag';
import 'rxjs/add/operator/toPromise';
import { environment } from '../../environments/environment';

@Injectable()
export class PostTagRepository {
  private url = environment.apiUrl + '/api/post-tags';

  constructor(private http: HttpClient) { }

  add(tagId: number, postId: number): Promise<Tag> {
    return this.http.post(this.url, { tagId: tagId, postId: postId })
      .toPromise()
      .then((res: Response) => {
        return res.json() as Tag;
      })
      .catch(this.handleError);
  }

  removeByCompound(tagId: number, postId: number): Promise<Response> {
    return this.http.post(this.url + '/remove/compound', { tagId: tagId, postId: postId })
      .toPromise()
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
}
