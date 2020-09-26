export interface IUser {
    id: string;
    displayName: string;
    token: string;
    image?: string;
}

export interface IUserLoginFormValues {
    email: string;
    password: string;
}

export interface IUserRegisterFormValues {
    email: string;
    password: string;
    birthday: Date;
    displayName: string;
    gender: string;
}
