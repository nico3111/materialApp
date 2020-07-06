function fetchData() {
    return fetch('http://192.168.0.94:8015/material/notebook')
    .then(response => response.json()).catch(err => console.log(err))
}

export{
    fetchData
}