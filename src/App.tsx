import CardBack from "./components/CardBack"
import { motion } from "framer-motion"
import { useState, useEffect } from "react"
import Card from "./components/Card"
import axios from "axios"

function App() {
  const [playerHand, setPlayerHand] = useState<number[]>([])
  const [enemyHand, setEnemyHand] = useState<number[]>([])
  const [enemyPass, setEnemyPass] = useState<boolean>(false)
  const [playerPass, setPlayerPass] = useState<boolean>(false)

  function getScore(pers : string){
    let score = 0;
    if (pers === 'player') {
      playerHand.forEach(card => {
        score += card
      })
    } else if (pers === 'enemy') {
      enemyHand.forEach(card => {
        score += card
      })
    }
    return score
  }

  function playerDrawsCard() {
    axios.get('https://localhost:44328/hand/pass').then(res => {
      // setEnemyPass(res.data["enemyPass"])
      // setPlayerPass(res.data["playerPass"])
      console.log(res.data);
      
    })

    if(!playerPass){
      axios.put('https://localhost:44328/hand/player').then(res => {
        setPlayerHand(res.data)
      })
    }

    setTimeout(() => {
      axios.put('https://localhost:44328/hand/enemy').then(res => {
        setEnemyHand(res.data)
      })
    }, 1000)
  }

  // useEffect that works only once on the first render and resetting server's variables
  useEffect(() => {
    axios.post('https://localhost:44328/hand/default')
  }, [])



  return (
    <div className="h-screen w-screen bg-gradient-radial from-[#0F947C] to-[#005344]">

      {/* Enemy's half */}
      <div className="h-[33%] flex items-center gap-52">
        <div className="w-28 h-28 border-2 border-white rounded-xl ml-16">
          <h1 className="break-words text-center text-xl text-white">Enemy's Score</h1>
          <h1 className="text-center text-white mt-5">{getScore('enemy') == 0 ? 0 : getScore('enemy') === enemyHand[0] ? `?` : `? + ${getScore('enemy') - enemyHand[0]}`} / 21</h1>
        </div>

        {/* enemy's hand */}
        <div className="flex gap-4">
          {enemyHand.map((card, index) => (

            index !== 0 ?

            <motion.div className="w-[120px]" key={index} initial={{x: -390 - 136 * index, y : 200}} transition={{duration : 0.7}} animate={{x : 0, y : 0}}>
              <motion.div transition={{duration : 0.15}} whileHover={{scale : 1.1}}>
                <Card value={card} />
              </motion.div>
            </motion.div>

            :

            <motion.div className="w-[120px]" key={index} initial={{x: -390 - 136 * index, y : 200}} transition={{duration : 0.7}} animate={{x : 0, y : 0}}>
              <motion.div transition={{duration : 0.15}} whileHover={{scale : 1.1}}>
                <CardBack />
              </motion.div>
            </motion.div>

          ))}
        </div>
      </div>

      {/* Deck */}
      <div className="h-[33%]">
        <div className="w-[120px]" onClick={() => {
          playerDrawsCard()
        }}>
          <CardBack />
        </div>


        <button className="w-28 h-10 border-2 border-white ml-1 rounded-2xl mt-5 text-white text-2xl shadow-lg shadow-black hover:shadow-sm hover:shadow-black hover:scale-[95%]" onClick={() => {
          axios.get("https://localhost:44328/hand/playerPass").then(res => {
            setPlayerPass(res.data); 
            console.log(res.data)})
        }}>Pass</button>

        
      </div>


      {/* Player's half */}
      <div className="h-[33%] flex items-center gap-52">
        <div className="w-28 h-28 border-2 border-white rounded-xl ml-16">
          <h1 className="break-words text-center text-xl text-white">Player's Score</h1>
          <h1 className="text-center text-white mt-5">{getScore('player')} / 21</h1>
        </div>

        {/* player's hand */}
        <div className="flex gap-4">
          {playerHand.map((card, index) => (
            <motion.div  className="w-[120px]" key={index} initial={{x: -390 - 136 * index, y : -280}} transition={{duration : 0.7}} animate={{x : 0, y : 0}}>
              <motion.div transition={{duration : 0.15}} whileHover={{scale : 1.1}}>
                <Card value={card} />
              </motion.div>
            </motion.div>
          ))}
        </div>
      </div>
    </div>
  )
}

export default App
