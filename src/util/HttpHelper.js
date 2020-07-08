function fetchPersons() {
    return fetch("http://192.168.0.94:8016/person")
    .then(response => response.json())
}

export{
    fetchPersons
}