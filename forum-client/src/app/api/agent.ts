import axios, {AxiosError, AxiosResponse} from "axios";
import Category from "../models/category";
import { PaginatedResult } from "../models/pagination";
import { Photo, Profile, ProfileUpdateData } from "../models/profile";
import { Topic, TopicFormValues } from "../models/topic";
import { User, UserFormValues } from "../models/user";
import { store } from "../stores/store";


axios.defaults.baseURL = 'http://localhost:5000/api'


axios.interceptors.request.use(config => {
    const token = store.userStore.token;

    if(token) config.headers.Authorization = `Bearer ${token}`
    return config;
})

axios.interceptors.response.use(async response => {  

    const pagination = response.headers['pagination'];
    if(pagination){
        response.data = new PaginatedResult(response.data, JSON.parse(pagination))
        return response as AxiosResponse<PaginatedResult<any>>
    }
    return response;
},(error: AxiosError) => {
    const {data, status, config} = error.response!;
    switch(status){
        case 400:

            if(data.errors){
                const modelStateErrors = [];

                for( const key in data.errors){
                    if(data.errors[key]){
                        modelStateErrors.push(data.errors[key]);
                    }
                }
                throw modelStateErrors.flat();
            }
            
            break;
        }
        return Promise.reject(error);
    })


const responseBody = <T>(response: AxiosResponse<T>) => response.data;


const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {} ) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const Topics = {
    list: (params: URLSearchParams) => axios.get<PaginatedResult<Topic[]>>('/Topics/all', {params}).then(responseBody),
    listByCategory: (categoryId: string, params: URLSearchParams) => axios.get<PaginatedResult<Topic[]>>(`/Topics/category/${categoryId}`, {params}).then(responseBody),
    details: (id: string) => requests.get<Topic>(`/Topics/${id}`),
    create: (topic: TopicFormValues) => requests.post('/Topics', topic),
    update: (topic: TopicFormValues) => requests.put(`/Topics`, topic),
    delete: (id: string) => requests.del<void>(`/Topics/${id}`),
    like: (id: string) => requests.post(`/Topics/${id}/like`, {})

}


const Categories = {
    list: () => axios.get<Category[]>('/Categories').then(responseBody),
    create: (category: Category) => requests.post('/Categories', category),
    update: (category: Category) => requests.put(`/Categories`, category),
    delete: (id: string) => requests.del<void>(`/Categories/${id}`),
}

const Account = {
    current: () => requests.get<User>('/account'),
    login: (user: UserFormValues) => requests.post<User>('/account/login',user),
    register: (user: UserFormValues) => requests.post<User>('/account/register',user)
}

const Profiles = {
    get: (username: string) => requests.get<Profile>(`/Profiles/${username}`),
    uploadPhoto: (file: Blob) => {
        let formData = new FormData();
        formData.append('File', file);
        return axios.post<Photo>('/photos', formData,{
            headers: {'Content-type': 'multipart/form-data'}
        })
    },
    updateProfile: (data: ProfileUpdateData) => requests.put<Profile>(`/profiles`,data),
    getTopics: (username: string) => requests.get<Topic[]>(`profiles/${username}/topics`),
    deletePhoto: (id: string) => requests.del(`/photos/${id}`)
}


const agent = {
    Topics,
    Categories,
    Account,
    Profiles
};

export default agent;