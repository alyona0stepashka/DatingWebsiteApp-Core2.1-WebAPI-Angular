import { StaticBase } from './static-base.model';

export class UserProfile {
    Id: string;
    Name: string;
    Email: string;
    IsAnonimus: boolean;
    PhotoPath: string;
    DateBirth: Date;
    Sex: StaticBase;
    MainGoal: StaticBase;
    FamilyStatus: StaticBase;
    FinanceStatus: StaticBase;
    Education: StaticBase;
    Nationality: StaticBase;
    Zodiac: StaticBase;
    Growth: number;
    Weight: number;
    Languages: StaticBase[];
    BadHabits: StaticBase[];
    Interests: StaticBase[];
}
