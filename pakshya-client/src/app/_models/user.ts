export interface IUser {
    username: string;
    displayName: string;
    token: string;
    image?: string
}

export interface IUserLoginFormValues {
    email: string;
    password: string;
}

export interface IUserRegisterFormValues {
    username: string;
    email: string;
    password: string;
    birthday: Date;
    displayName: string;
    gender: string
}