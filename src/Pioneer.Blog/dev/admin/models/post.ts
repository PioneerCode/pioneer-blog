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
  modifiedOn: string;
  tags: Tag.Tag[];
  article: Article.Article;
  category: Category.Category;
  excerpt: Excerpt.Excerpt;
}
