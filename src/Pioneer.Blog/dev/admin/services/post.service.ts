//namespace Pioneer.Services {
//    "use strict";

//    export interface IPostService {
//        getPosts(): Models.Post[];
//    }

//    class PostService implements IPostService {
//        posts = [] as Models.Post[];

//        constructor(
//            private pioneerRepository: Repository.IApiRepository
//        ) {
//            this.pioneerRepository.getPosts(5)
//                .then((resp: Models.Post[]) => {
//                    this.posts = resp;
//                });
//        }

//        getPosts(): Models.Post[] {
//            return this.posts;
//        }
//    }

//    factory.$inject = [
//        "pioneer.repository.apiRepository"
//    ];

//    function factory(
//        apiRepository: Repository.IApiRepository
//    ): IPostService {
//        return new PostService(
//            apiRepository
//        );
//    }

//    angular
//        .module("pioneer.services")
//        .factory("postService", factory);
//}