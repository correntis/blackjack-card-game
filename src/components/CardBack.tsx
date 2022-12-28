import React from 'react'
import back from '../imgs/cardBack.png'

const cardBack = () => {
  return (
    <div className="w-full shadow-card rounded-xl">
        <img src={back} alt="cardBack" className=""/>
    </div>
  )
}

export default cardBack