import { Photo } from "./photo";

export interface Member {
    id: number;
    UserName: string;
    age: number;
    photoUrl: string;
    knownAs: string;
    created: Date;
    lastActive: Date;
    gender: string;
    introduction: string;
    interests: string;
    lookingFor: string;
    city: string;
    country: string;
    photos: Photo[];
  }
  