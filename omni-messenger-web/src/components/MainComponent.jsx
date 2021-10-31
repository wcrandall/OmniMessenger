import { statuses, useContactInfo } from "../hooks/useContactInfo";

function MainComponent(){

  const [data, status, error] = useContactInfo();

  if(error ===  statuses.failure){
    return <div>ERROR: {error}</div>
  }
  
  if(status === statuses.loading){
    return <div>Loading</div>;
  }
  
  const fullName =  data === undefined ||
                    data[0] === undefined ?
                    "" : data[0].fullName; 
  
  const onButtonClick = async function onButtonClick(){
    console.log("go fuck yourself");
  }

  return(
      <div className="App">
          <p>Message is to</p>
          <textarea row="4" cols="50" />
          <br/>
          <button onClick={onButtonClick}>Send message to { fullName }</button>
      </div>
  );

}

export default MainComponent;