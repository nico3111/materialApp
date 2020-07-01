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
        this.setState({ notebook: data, loading: false });
    }

    render() {
        console.log(this.state)
        return (
            <div>
                {this.state.loading ? <div>loading...</div> : <div> 
                   
                    {this.state.notebook[0].serial_number} 
                </div>}
            </div>
        )
    }
}
