import { useEffect, useState } from "react";

const Home = () => {
  const [data, setData] = useState([]);
  const getListData = async () => {
    const res = await fetch('https://jsonplaceholder.typicode.com/posts');
    const listData = await res.json();
    setData(listData);
    console.log(listData);
  }

  //please refer https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch
  //for more details
  useEffect(() => {
    getListData();
  }, [])
  return (
    <>
      <h1>Home</h1>
      {
        data.map(item => (
            <div><p>Id: {item.id} </p>
            <p>Title: {item.title}</p></div>
          )
        )
      }
    </>
  )
};

export default Home;