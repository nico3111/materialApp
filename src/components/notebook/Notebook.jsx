import React from 'react';
import '../notebook/Notebook.css'

var notebooks = [];

export default class Notebook extends React.Component {

    constructor(props) {
        super(props)
        this.state = {
            notebook: [
            ]
        }
    }

    handleChange = e => {
        let notebook = { ...this.state.notebook };
        notebook[e.target.name] = e.target.value;
        this.setState({ notebook });
    };

    handleSubmit = e => {
        e.preventDefault();

        notebooks.push(this.state.notebook);
        
        e.target.reset();
        console.log(notebooks)
    }

    render() {

        return (
            <>
                <div className="main-wrapper">
                    <div>Geben Sie ein neues Notebook ein:</div>
                    <form onSubmit={this.handleSubmit}>
                        <input name="serialNumber" value={this.state.serialNumber} onChange={this.handleChange} placeholder="Serien Nummer" required/>
                        <input name="make" value={this.state.make} onChange={this.handleChange} placeholder="Marke" required/>
                        <input name="model" value={this.state.model} onChange={this.handleChange} placeholder="Modell" required/>
                        <input name="location" value={this.state.location} onChange={this.handleChange} placeholder="Standort" required/>
                        <input name="person" value={this.state.person} onChange={this.handleChange} placeholder="Person"/>
                        <input type="submit" value="submit" />
                    </form>
                </div>
            </>
        )
    }
}
