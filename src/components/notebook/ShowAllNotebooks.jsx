import React from 'react';
import '../notebook/AllNotebook.css'


export default class ShowAllNotebook extends React.Component {

    state = {
        loading: true,
        allnotebooks: [],
    };

    async componentDidMount() {
        const url = "http://localhost:5000/Material/Notebook";
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ allnotebooks: data});
    }

    render() {
<<<<<<< HEAD

        if (this.props.isShowing === false) {
            return null;
        }

=======
>>>>>>> 2c785232780d213e3edbb7469c17f91393316978
        return (
            <div className="notebooks-wrapper">
                {this.state.allnotebooks.map(allnotebooks => (
                    <div className="notebooks">
                    <div className="show-list">
                        <div>Seriennummer: {allnotebooks.serial_number}</div><br></br>
                        <div>Marke: {allnotebooks.make}</div><br></br>
                        <div>Modell: {allnotebooks.model}</div><br></br>
                        <div>Person: {allnotebooks.person}</div><br></br>
                        <div>Standort: {allnotebooks.location}</div><br></br>
                    </div>
                    </div>))}
            </div>
        )
    }
}
