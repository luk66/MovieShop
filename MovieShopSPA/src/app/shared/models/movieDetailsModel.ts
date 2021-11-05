

    export interface Cast {
        id: number;
        name: string;
        gender?: any;
        tmdbUrl?: any;
        profilePath: string;
        character: string;
    }

    export interface Genre {
        id: number;
        name: string;
    }

    export interface Trailer {
        id: number;
        movieId: number;
        trailerUrl: string;
        name: string;
    }

    export interface MovieDetailsModel {
        id: number;
        title: string;
        posterUrl: string;
        backdropUrl: string;
        rating: number;
        overview: string;
        tagline: string;
        budget: number;
        revenue: number;
        imdbUrl: string;
        tmdbUrl: string;
        releaseDate: Date;
        runTime: number;
        price: number;
        isPurchased?: any;
        isReviewed?: any;
        isFavorited?: any;
        casts: Cast[];
        genres: Genre[];
        reviews: any[];
        trailers: Trailer[];
    }



