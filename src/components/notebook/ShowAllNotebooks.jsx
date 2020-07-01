import React from 'react';
import '../notebook/Notebook.css'


export default class ShowAllNotebook extends React.Component {

    state = {
        loading: true,
        notebook: null,
    };

    async componentDidMount() {
        const url = "https://localhost:44323/Material/Notebook";
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ person: data.results, loading: false });

    }

    render() {
        return (
            <div>
                {this.state.loading || !this.state.notebook ? (<div>loading...</div>) : (<div> {this.state.notebook.serial_number} </div>)}
            </div>
        )
    }
}
