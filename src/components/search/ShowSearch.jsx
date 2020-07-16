import React from 'react';
import '../notebook/SaveNewNotebook.css';
import '../search/Search.css';
const { fetchSearch } = require('../../util/HttpHelper')

export default class ShowSearch extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            searchKeyWord: null,
            searchResult: [],
        }
    }

    // async componentDidMount() {
    //     this.fetchSearch()
    // }

    // fetchSearch = async () => {
    //     const searchResult = await fetchSearch()
    //     this.setState({ searchResult: searchResult });
    // }

    updateWithEvent(event) {
        const key = event.target.name;
        const value = event.target.value;

        this.setState({
            [key]: value
        })
    }


    async postData() {
        
        const body = {
            searchKeyWord: this.state.searchKeyWord
        }

        try {
            var req = {
                method: 'post',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(body)
            }

            let result = await fetch('http://192.168.0.94:8015/material/search', req)
            this.searchResult = await result.text()
            console.log(this.searchResult)
         

            this.setState({
              searchKeyWord: ''
            })

            console.log(body)
        } catch (error) {
            alert(error)
        }
    }


    render() {
        return(
            <div className="main-wrapper-search">
            <div className="input-wrapper-search">

                <input className="input-field-search" value={this.state.searchKeyWord} required name="searchKeyWord" onChange={(event) => this.updateWithEvent(event)} placeholder="Suche"></input>
              

            
      
            <div type="submit" className="add-button-search" onClick={() => this.postData()}>Suchen</div>
        </div>
        </div>
        )
    }
}