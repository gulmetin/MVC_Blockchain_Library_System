using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Model.Class;
using DataAccessLayer.Model.Entity;

namespace BlockChainLayer
{
    public class BlockchainIslemleri//blockchainle ilgili işlemlerin bulunduğu class
    {
        BlockChainDBIslemleri blckDb = new BlockChainDBIslemleri();
        public Blockchain loadChain()
        {
            var bloklar = blckDb.getAllBlock();
            Blockchain blockchain = new Blockchain();
            for (int i = 0; i < bloklar.Count; i++)
            {
                Block block = new Block(bloklar[i].id, bloklar[i].timestamp.ToString(), bloklar[i].data, bloklar[i].hashPrev);
                block.hash = bloklar[i].hash;
                blockchain.chain.Add(block);
            }



            /*Blockchain blockchain = new Blockchain();
            Block genesisBlock = new Block(degerler[0].id, degerler[0].timestamp.ToString(), degerler[0].data, degerler[0].hashPrev);
            genesisBlock.hash = genesisBlock.calculateHash();
            blockchain.chain.Add(genesisBlock);
            for (int i = 1; i < degerler.Count; i++)
            {
                Block block = new Block(degerler[i].id, degerler[i].timestamp.ToString(), degerler[i].data, degerler[i].hashPrev);
                block.hash = degerler[i].hash;
                blockchain.addBlock(block);
            }*/
            return blockchain;
        }

        

    }
}
