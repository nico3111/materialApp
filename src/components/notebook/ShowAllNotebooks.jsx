import React from 'react';
import '../notebook/Notebook.css'

var notebooks = [];

export default class ShowAllNotebook extends React.Component {

    state = {
        loading: true,
        person: null,
    };

    componentDidMount() {
        const url = "https://localhost:44323/Material/Notebook/";
        async const response = await fetch(url);
        const data = await response.json();
        this.setState({ person: data.results[0], loading: false });

    }


    render() {
        return (
            <div>

                {this.state.loading || this.state.person ? (<div>loading...</div>) : (<div> {this.state.person.serial_number} </div>)}
            </div>
        )
    }
}
