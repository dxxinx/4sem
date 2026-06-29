// Базовые типы
type User = {
    name: string;
    age: number;
};

type UserLocation = {
    city: string;
    country: string;
};

type Department = {
    faculty: string;
    group: number;
};

type Professor = {
    name: string;
    degree: string;
};

type Article = {
    title: string;
    pagesNumber: number;
};

type Exam = {
    maths?: boolean;
    programming?: boolean;
    mark: number;
};

type ExamWithProfessor = Exam & {
    professor: Professor;
};

type ExamWithProfessorArticles = Exam & {
    professor: Professor & {
        articles: Article[];
    };
};

type Studies = {
    university: string;
    speciality: string;
    year: number;
};

type Student = {
    id: number;
    name: string;
    group: number;
};

type Post = {
    id: number;
    message: string;
    likesCount: number;
};

type Dialog = {
    id: number;
    name: string;
};

type Message = {
    id: number;
    message: string;
};

type ProfilePage = {
    posts: Post[];
    newPostText: string;
};

type DialogsPage = {
    dialogs: Dialog[];
    messages: Message[];
};

type Store = {
    state: {
        profilePage: ProfilePage;
        dialogsPage: DialogsPage;
        sidebar: any[];
    };
};

// Типизированный код
let user: User = {
    name: 'Masha',
    age: 21
};
let copyUser: User = { ...user }; 
console.log(copyUser);

let numbers: number[] = [1, 2, 3];
let copyNumbers: number[] = [...numbers]; 
console.log(copyNumbers);

let user1: User & { location: UserLocation } = {
    name: 'Masha',
    age: 23,
    location: {
        city: 'Minsk',
        country: 'Belarus'
    }
};
let user1Copy: User & { location: UserLocation } = { ...user1, location: { ...user1.location } };
console.log(user1Copy);

let user2: User & { skills: string[] } = {
    name: 'Masha',
    age: 28,
    skills: ["HTML", "CSS", "JavaScript", "React"]
};
let user2Copy: User & { skills: string[] } = { ...user2, skills: [...user2.skills] };
console.log(user2Copy);

const array: Student[] = [
    {id: 1, name: 'Vasya', group: 10}, 
    {id: 2, name: 'Ivan', group: 11},
    {id: 3, name: 'Masha', group: 12},
    {id: 4, name: 'Petya', group: 10},
    {id: 5, name: 'Kira', group: 11},
];
const copyArray: Student[] = array.map(item => ({ ...item }));
console.log(copyArray);

let user4: User & { 
    studies: Studies & { 
        exams: { 
            maths: boolean; 
            programming: boolean 
        } 
    } 
} = {
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
let user4Copy: typeof user4 = { 
    ...user4, 
    studies: { 
        ...user4.studies, 
        exams: { ...user4.studies.exams }
    }
};
console.log(user4Copy);

let user5: User & { 
    studies: Studies & { 
        department: Department;
        exams: Exam[];
    } 
} = {
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
            { maths: true, mark: 8},
            { programming: true, mark: 4},
        ]
    }
};
let user5Copy: typeof user5 = {
    ...user5, 
    studies: { 
        ...user5.studies, 
        department: { ...user5.studies.department },
        exams: user5.studies.exams.map(exam => ({ ...exam }))
    }
};
user5Copy.studies.department.group = 12;
user5Copy.studies.exams[1].mark = 10;
console.log(user5Copy);
console.log(user5.studies.department.group); 
console.log(user5.studies.exams[1].mark); 

let user6: User & { 
    studies: Studies & { 
        department: Department;
        exams: ExamWithProfessor[];
    } 
} = {
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
let user6Copy: typeof user6 = {
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
user6Copy.studies.exams[0].professor.name = 'Ivan Petrov';
user6Copy.studies.exams[1].professor.name = 'Petr Ivanov';
console.log(user6Copy);

let user7: User & { 
    studies: Studies & { 
        department: Department;
        exams: (Exam & {
            professor: Professor & {
                articles: Article[];
            };
        })[];
    } 
} = {
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
                        {title: "About HTML", pagesNumber: 3},
                        {title: "About CSS", pagesNumber: 5},
                        {title: "About JavaScript", pagesNumber: 1},
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
                        {title: "About HTML", pagesNumber: 3},
                        {title: "About CSS", pagesNumber: 5},
                        {title: "About JavaScript", pagesNumber: 1},
                    ]
                }
            },
        ]
    }
};
let user7Copy: typeof user7 = {
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
user7Copy.studies.exams[1].professor.articles[1].pagesNumber = 3;
console.log(user7Copy);

let store: Store = {
    state: {
        profilePage: {
            posts: [
                {id: 1, message: 'Hi', likesCount: 12},
                {id: 2, message: 'By', likesCount: 1},
            ],
            newPostText: 'About me',
        },
        dialogsPage: {
            dialogs: [
                {id: 1, name: 'Valera'},
                {id: 2, name: 'Andrey'},
                {id: 3, name: 'Sasha'},
                {id: 4, name: 'Viktor'},
            ],
            messages: [
                {id: 1, message: 'hi'},
                {id: 2, message: 'hi hi'},
                {id: 3, message: 'hi hi hi'},
            ],
        },
        sidebar: [],
    },
};

let storeCopy: Store = {
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

storeCopy.state.profilePage.posts.forEach(post => {
    post.message = 'Hello';
});
storeCopy.state.dialogsPage.messages.forEach(message => {
    message.message = 'Hello';
});

console.log(storeCopy);
console.log(store.state.profilePage.posts[0].message); 
console.log(store.state.dialogsPage.messages[0].message);