// 1
interface IUser {
    name: string;
    age: number;
}

let user: IUser = {
    name: 'Masha',
    age: 21
};

let copyUser: IUser = { ...user };
console.log(copyUser);


// 2
let numbers: number[] = [1, 2, 3];
let copyNumbers: number[] = [...numbers];
console.log(copyNumbers);


// 3
interface ILocation {
    city: string;
    country: string;
}

interface IUserWithLocation {
    name: string;
    age: number;
    location: ILocation;
}

let user1: IUserWithLocation = {
    name: 'Masha',
    age: 23,
    location: {
        city: 'Minsk',
        country: 'Belarus'
    }
};

let user1Copy: IUserWithLocation = {
    ...user1,
    location: { ...user1.location }
};

console.log(user1Copy);


// 4
interface IUserWithSkills {
    name: string;
    age: number;
    skills: string[];
}

let user2: IUserWithSkills = {
    name: 'Masha',
    age: 28,
    skills: ["HTML", "CSS", "JavaScript", "React"]
};

let user2Copy: IUserWithSkills = {
    ...user2,
    skills: [...user2.skills]
};

console.log(user2Copy);


// 5
interface IStudent {
    id: number;
    name: string;
    group: number;
}

const array: IStudent[] = [
    { id: 1, name: 'Vasya', group: 10 },
    { id: 2, name: 'Ivan', group: 11 },
    { id: 3, name: 'Masha', group: 12 },
    { id: 4, name: 'Petya', group: 10 },
    { id: 5, name: 'Kira', group: 11 },
];

const copyArray: IStudent[] = array.map(item => ({ ...item }));
console.log(copyArray);


// 6
interface IExams {
    maths: boolean;
    programming: boolean;
}

interface IStudies {
    university: string;
    speciality: string;
    year: number;
    exams: IExams;
}

interface IUser4 {
    name: string;
    age: number;
    studies: IStudies;
}

let user4: IUser4 = {
    name: 'Masha',
    age: 19,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        exams: {
            maths: true,
            programming: false
        }
    }
};

let user4Copy: IUser4 = {
    ...user4,
    studies: {
        ...user4.studies,
        exams: { ...user4.studies.exams }
    }
};

console.log(user4Copy);


// 7
interface IDepartment {
    faculty: string;
    group: number;
}

interface IExamResult {
    maths?: boolean;
    programming?: boolean;
    mark: number;
}

interface IUser5Studies {
    university: string;
    speciality: string;
    year: number;
    department: IDepartment;
    exams: IExamResult[];
}

interface IUser5 {
    name: string;
    age: number;
    studies: IUser5Studies;
}

let user5: IUser5 = {
    name: 'Masha',
    age: 22,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        department: {
            faculty: 'FIT',
            group: 10,
        },
        exams: [
            { maths: true, mark: 8 },
            { programming: true, mark: 4 },
        ]
    }
};

let user5Copy: IUser5 = {
    ...user5,
    studies: {
        ...user5.studies,
        department: { ...user5.studies.department },
        exams: user5.studies.exams.map(exam => ({ ...exam }))
    }
};

user5Copy.studies.department.group = 12;
user5Copy.studies.exams[1]!.mark = 10;

console.log(user5Copy);
console.log(user5.studies.department.group);
console.log(user5.studies.exams[1]!.mark);


// 8
interface IProfessor {
    name: string;
    degree: string;
}

interface IExamWithProfessor extends IExamResult {
    professor: IProfessor;
}

interface IUser6Studies extends IUser5Studies {
    exams: IExamWithProfessor[];
}

interface IUser6 {
    name: string;
    age: number;
    studies: IUser6Studies;
}

let user6: IUser6 = {
    name: 'Masha',
    age: 21,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        department: {
            faculty: 'FIT',
            group: 10,
        },
        exams: [
            {
                maths: true,
                mark: 8,
                professor: {
                    name: 'Ivan Ivanov',
                    degree: 'PhD'
                }
            },
            {
                programming: true,
                mark: 10,
                professor: {
                    name: 'Petr Petrov',
                    degree: 'PhD'
                }
            },
        ]
    }
};

let user6Copy: IUser6 = {
    ...user6,
    studies: {
        ...user6.studies,
        department: { ...user6.studies.department },
        exams: user6.studies.exams.map(exam => ({
            ...exam,
            professor: { ...exam.professor }
        }))
    }
};

console.log(user6Copy);


// 9
interface IArticle {
    title: string;
    pagesNumber: number;
}

interface IProfessorWithArticles extends IProfessor {
    articles: IArticle[];
}

interface IExamWithArticles extends IExamResult {
    professor: IProfessorWithArticles;
}

interface IUser7Studies extends IUser5Studies {
    exams: IExamWithArticles[];
}

interface IUser7 {
    name: string;
    age: number;
    studies: IUser7Studies;
}

let user7: IUser7 = {
    name: 'Masha',
    age: 20,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        department: {
            faculty: 'FIT',
            group: 10,
        },
        exams: [
            {
                maths: true,
                mark: 8,
                professor: {
                    name: 'Ivan Petrov',
                    degree: 'PhD',
                    articles: [
                        { title: "About HTML", pagesNumber: 3 },
                        { title: "About CSS", pagesNumber: 5 },
                        { title: "About JavaScript", pagesNumber: 1 },
                    ]
                }
            },
            {
                programming: true,
                mark: 10,
                professor: {
                    name: 'Petr Ivanov',
                    degree: 'PhD',
                    articles: [
                        { title: "About HTML", pagesNumber: 3 },
                        { title: "About CSS", pagesNumber: 5 },
                        { title: "About JavaScript", pagesNumber: 1 },
                    ]
                }
            },
        ]
    }
};

let user7Copy: IUser7 = {
    ...user7,
    studies: {
        ...user7.studies,
        department: { ...user7.studies.department },
        exams: user7.studies.exams.map(exam => ({
            ...exam,
            professor: {
                ...exam.professor,
                articles: exam.professor.articles.map(article => ({ ...article }))
            }
        }))
    }
};

console.log(user7Copy);


// 10 STORE
interface IPost {
    id: number;
    message: string;
    likesCount: number;
}

interface IDialog {
    id: number;
    name: string;
}

interface IMessage {
    id: number;
    message: string;
}

interface IProfilePage {
    posts: IPost[];
    newPostText: string;
}

interface IDialogsPage {
    dialogs: IDialog[];
    messages: IMessage[];
}

interface IState {
    profilePage: IProfilePage;
    dialogsPage: IDialogsPage;
    sidebar: unknown[];
}

interface IStore {
    state: IState;
}

let store: IStore = {
    state: {
        profilePage: {
            posts: [
                { id: 1, message: 'Hi', likesCount: 12 },
                { id: 2, message: 'By', likesCount: 1 },
            ],
            newPostText: 'About me',
        },
        dialogsPage: {
            dialogs: [
                { id: 1, name: 'Valera' },
                { id: 2, name: 'Andrey' },
                { id: 3, name: 'Sasha' },
                { id: 4, name: 'Viktor' },
            ],
            messages: [
                { id: 1, message: 'hi' },
                { id: 2, message: 'hi hi' },
                { id: 3, message: 'hi hi hi' },
            ],
        },
        sidebar: [],
    },
};

let storeCopy: IStore = {
    state: {
        ...store.state,
        profilePage: {
            ...store.state.profilePage,
            posts: store.state.profilePage.posts.map(post => ({ ...post }))
        },
        dialogsPage: {
            ...store.state.dialogsPage,
            dialogs: store.state.dialogsPage.dialogs.map(dialog => ({ ...dialog })),
            messages: store.state.dialogsPage.messages.map(message => ({ ...message }))
        },
        sidebar: [...store.state.sidebar]
    }
};

console.log(storeCopy);
