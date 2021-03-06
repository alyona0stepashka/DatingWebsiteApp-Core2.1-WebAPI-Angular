import { StaticBase } from './static-base.model';

export class UserProfile {
    Id = '';
    Name = '';
    Email = '';
    IsAnonimus: boolean = null;
    IsOnline: any = null;
    IsFriend: boolean = null;
    IsBlack: boolean = null;
    IsIBlack: boolean = null;
    PhotoPath = '';
    DateBirth: Date = null;
    Sex: StaticBase = null;
    MainGoal: StaticBase = null;
    FamilyStatus: StaticBase = null;
    FinanceStatus: StaticBase = null;
    Education: StaticBase = null;
    Nationality: StaticBase = null;
    Zodiac: StaticBase = null;
    Age: number = null;
    Growth: number = null;
    Weight: number = null;
    Views: number = null;
    ReplyRate: number = null;
    Languages: StaticBase[] = new Array();
    BadHabits: StaticBase[] = new Array();
    Interests: StaticBase[] = new Array();
}
