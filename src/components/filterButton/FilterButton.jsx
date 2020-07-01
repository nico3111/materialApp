import React from 'react'
import './FilterButton.css'

export default class FilterButton extends React.Component {
    
    renderActiveButton = () => {
        return (
        <div className="filter-buttons is-active" onClick={this.props.onClick}>
           {this.props.title}
        </div>
        )
    }

    renderInactiveButton = () => {
        return(
        <div className="filter-buttons" onClick={this.props.onClick}>
           {this.props.title}
        </div>
        )
    }
    
    render() {

        // const title = this.props.title;
        const isActive = this.props.isActive

        if (isActive) {
            return (this.renderActiveButton())
        } 
        return this.renderInactiveButton()

        // return (
        //   <div className={"filter-buttons " + (isActive ? 'is-active' : '')} onClick={this.props.onClick}>
        //       {title}
        // </div>
        // )
    }
}