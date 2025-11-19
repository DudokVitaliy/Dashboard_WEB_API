export interface GameImage {
    id: string;
    imagePath: string;
    isMain: boolean;
}
export interface Game {
    id: string;
    name: string;
    description: string;
    price: number;
    releaseDate: Date;
    publisher: string;
    developer: string;
    mainImage: GameImage | null;
    images: GameImage[];
}