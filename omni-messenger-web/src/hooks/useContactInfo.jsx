import {useState, useEffect} from 'react'; 

const statuses = Object.freeze({
  loading: "loading",
  success: "success",
  failure: "failure"
});

function useContactInfo(){
    const [data, setData] = useState([]);
    const [status, setStatus] = useState(statuses.loading);
    const [error, setError] = useState("");
  
    useEffect(()=>{
      try{
        fetch("https://localhost:5001/message")
        .then(res => res.json())
        .then(result => {
          setData(result);
          setStatus(statuses.success);
        });
      }
      catch(e){
        setStatus(statuses.failure);
        setError(e);
      }
  
    }, []);

    return [data, status, error];
}

export {statuses, useContactInfo};