import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Post } from '../models/post';
import 'rxjs/add/operator/toPromise';
import { environment } from '../../environments/environment';

@Injectable()
export class PostRepository {
  private url = environment.apiUrl + '/api/posts';

  constructor(private http: HttpClient) { }

  get(idUrl: string, includeExcerpt = false): Promise<Post> {
    return this.http.get(this.url + '/' + idUrl + '?includeExcerpt=' + includeExcerpt, { withCredentials: true })
      .toPromise()
      .then((res: Response) => {
        const body: Post = res.json();
        return body || {} as Post;
      })
      .catch(this.handleError);
  }

  getAll(count: number = null,
    page: number = null,
    includeExcerpt = true,
    includeArticle = true,
    includeUnpublished = true): Promise<Post[]> {

    const query = `?countPerPage=${count}` +
      `&currentPageIndex=${page}` +
      `&includeExcerpt=${includeExcerpt}` +
      `&includeArticle=${includeArticle}` +
      `&includeUnpublished=${includeUnpublished}`;

    return this.http.get(this.url + query)
      .toPromise()
      .then((res: Response) => {
        const body: Post[] = res.json();
        return body || [] as Post[];
      })
      .catch(this.handleError);
  }

  create(): Promise<Post> {
    return this.http.post(this.url, {} as Post, {
      withCredentials: true
    })
      .toPromise()
      .then((res: Response) => {
        const body: Post = res.json();
        return body || [];
      })
      .catch(this.handleError);
  }

  save(post: Post): Promise<Response> {
    return this.http.put(this.url + '\\' + post.postId, post)
      .toPromise()
      .catch(this.handleError);
  }

  remove(idUrl: string): Promise<Response> {
    return this.http.delete(this.url + '\\' + idUrl)
      .toPromise()
      .catch(this.handleError);
  }

  getTotalNumberOfPosts(): Promise<number> {
    return this.http.get(this.url + '\\count\\total')
      .toPromise()
      .then((res: Response) => {
        return res.json();
      })
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
}
