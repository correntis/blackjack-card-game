import React, { useEffect } from 'react'
import card1 from '../imgs/card1.png'
import card2 from '../imgs/card2.png'
import card3 from '../imgs/card3.png'
import card4 from '../imgs/card4.png'
import card5 from '../imgs/card5.png'
import card6 from '../imgs/card6.png'
import card7 from '../imgs/card7.png'
import card8 from '../imgs/card8.png'
import card9 from '../imgs/card9.png'
import card10 from '../imgs/card10.png'
import card11 from '../imgs/card11.png'

interface CardProps {
    value: number
}

const Card = ({value} : CardProps) => {

    let cards = [card1, card2, card3, card4, card5, card6, card7, card8, card9, card10, card11]


  return (
    <div className='shadow-card rounded-xl'>
        <img src={cards[value - 1]} alt="card" />
    </div>
  )
}

export default Card