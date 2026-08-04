import { jwtDecode } from "jwt-decode";


interface JwtPayload {

    actor_type:string;

    sub:string;

    email:string;

}


export const getActorType = ()=>{

    const token = localStorage.getItem("token");

    if(!token)
        return null;


    const decoded = jwtDecode<JwtPayload>(token);

    return decoded.actor_type;

};


export const getUserId = ()=>{

    const token = localStorage.getItem("token");

    if(!token)
        return null;


    const decoded = jwtDecode<JwtPayload>(token);

    return decoded.sub;

};