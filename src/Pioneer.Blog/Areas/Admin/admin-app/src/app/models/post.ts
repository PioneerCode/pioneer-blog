import { Tag } from "./tag";
import { Category } from "./category";
import { Article } from "./article";
import { Excerpt } from "./excerpt";

export enum PreviousCurrentNextPosition {
  Previous = 0,
  Current,
  Next
};

export class Post {
  postId: number;
  title: string;
  meta: string;
  description: string;
  image: string;
  smallImage: string;
  iconImage: string;
  url: string;
  link: string;
  postedOn: string;
  modifiedOn: string;
  tags: Tag[];
  article: Article;
  category: Category;
  excerpt: Excerpt;
}
