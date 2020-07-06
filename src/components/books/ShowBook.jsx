import React from 'react';
import '../notebook/AllNotebook.css'
import AddBook from './AddBook';

export default class ShowBook extends React.Component {

    state = {
        allBooks: []
    };



    async componentDidMount() {
        this.fetchDisplays()
    }

    fetchDisplays = async () => {
        const url = "http://192.168.0.94:8015/material/book";
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ allBooks: data });
    }

    async deleteData(id) {

        if (window.confirm("Möchten Sie wirklich löschen")) {

            try {
                fetch("http://192.168.0.94:8015/material/book/" + id, {
                    method: 'delete',
                    mode: 'cors'
                }).then(this.fetchDisplays)
    
            } catch (error) {
                console.log(error)
            }
        }
    }


    render() {
        if (this.props.isShowing === false) {
            return null;
        }
        return (
            <div className="notebooks-wrapper">

                <div className="line-wrapper">
                    <div className="line"></div>
                    <div className="line-text">Bücher</div>
                    <div className="line2"></div>
                </div>

                {this.state.allBooks.map(allBooks => (
                    <div className="notebooks">
                        <div className="show-list">
                            <div className="head-text">Buch</div>
                            <div>Titel: {allBooks.title}</div><br></br>
                            <div>ISBN: {allBooks.isbn}</div><br></br>
                            <div>Person: {allBooks.person != null ? allBooks.person.name1 + " " + allBooks.person.name2 : ""}</div><br></br>
                            <div>Standort: {allBooks.classroom != null ? allBooks.classroom.addressloc.address.place : ""}</div><br></br>
                            <div>Räumlichkeit: {allBooks.classroom != null ? allBooks.classroom.room : ""}</div><br></br>
                        
                            <div className="button-wrapper">
                                <div className="add-button2" onClick={() => this.deleteData(allBooks.id)}>Löschen</div>
                                <div className="add-button2" onClick={() => this.deleteData(allBooks.id)}>Update</div>
                            </div>
                        
                        </div>
                    </div>))}
                <AddBook fetchDisplays={this.fetchDisplays} />
            </div>
        )
    }
}