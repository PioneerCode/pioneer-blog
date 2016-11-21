import Tag = require('./tag');
import Article = require('./article');
import Category = require('./category');
import Excerpt = require('./excerpt');

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
  modifedOn: string;
  tags: Tag.Tag[];
  article: Article.Article;
  cateogry: Category.Category;
  excerpt: Excerpt.Exceprt;
}