import { StaticBase } from './static-base.model';

export class Static {
    BadHabits: StaticBase[];
    Educations: StaticBase[];
    FamilyStatuses: StaticBase[];
    FinanceStatuses: StaticBase[];
    Interests: StaticBase[];
    Languages: StaticBase[];
    MainGoals: StaticBase[];
    Nationalities: StaticBase[];
    Sexes: StaticBase[];
    Zodiacs: StaticBase[];
    constructor(){        
    this.BadHabits = []; 
    this.Educations = [];
    this.FamilyStatuses = [];
    this.FinanceStatuses = [];
    this.Interests = [];
    this.Languages = [];
    this.MainGoals = [];
    this.Nationalities = [];
    this.Sexes = [];
    this.Zodiacs = [];
    }
}
