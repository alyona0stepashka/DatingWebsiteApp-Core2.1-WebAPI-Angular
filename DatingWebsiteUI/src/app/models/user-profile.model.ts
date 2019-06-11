import { StaticBase } from './static-base.model';

export class UserProfile {
    Id: string = '';
    Name: string = '';
    Email: string = '';
    IsAnonimus: boolean = null;
    IsFriend: boolean = null;
    IsBlack: boolean=null;
    PhotoPath: string= '';
    DateBirth: Date=null;
    Sex: StaticBase=null;
    MainGoal: StaticBase=null;
    FamilyStatus: StaticBase=null;
    FinanceStatus: StaticBase=null;
    Education: StaticBase=null;
    Nationality: StaticBase=null;
    Zodiac: StaticBase=null;
    Growth: number=null;
    Weight: number=null;
    Languages: StaticBase[]=null;
    BadHabits: StaticBase[]=null;
    Interests: StaticBase[]=null;
}
