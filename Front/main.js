

/* const DataMethod = {
    method: 'GET'
};

const DataConexion = (DataMethod) => {
        return fetch(`http://localhost:5150/PersonType`, DataMethod)
        .then((response) => response.json())
        .then((json) => json)
        .catch((error) => console.error('Error:', error));
};

DataConexion(DataMethod)
    .then((data) => console.log(data))
    .catch((error) => console.error('Error:', error)); */


const DataMethod = {
    method: 'GET'
};
const DataConexion = async (DataMethod) => {
    try {
        const peticion = await fetch(`http://localhost:5150/City`, DataMethod);
        const json = await peticion.json();
        return json;
    } catch (error) {
        console.error('Error:', error);
    }
};

(async () => {
    const data = await DataConexion(DataMethod);
    console.log(data);
})();