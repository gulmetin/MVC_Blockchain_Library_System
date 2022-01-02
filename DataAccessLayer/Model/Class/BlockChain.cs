using DataAccessLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Model.Class
{
    public class Blockchain
    {
        HareketDBIslemleri hrktDb = new HareketDBIslemleri();

        public List<Block> chain = new List<Block>();

        public Block createGenesisBlock()
        {
            return new Block(0, "1/12/2021", "Genesis block", "0");
        }

        public Block getLatestBlock()
        {
            return this.chain[this.chain.Count - 1];
        }

        public void addBlock(Block newBlock)
        {
            newBlock.prevHash = this.getLatestBlock().hash;
            newBlock.hash = newBlock.calculateHash();
            this.chain.Add(newBlock);
        }
        public bool isChainValid()
        {
            for (int i = 1; i < this.chain.Count; i++)
            {
                Block currentBlock = this.chain[i];
                Block prevBlock = this.chain[i - 1];
                List<tblHareket> islemler = hrktDb.getAllHareketByDay(Convert.ToDateTime(currentBlock.timestamp));
                string transactionData = "";
                for (int j = 0; j < islemler.Count; i++)
                {
                    transactionData += (islemler[j].UYE.ToString() + islemler[j].KITAP.ToString() + islemler[j].PERSONEL.ToString() + islemler[j].ISLEMTARIHI.ToString() + islemler[j].ISLEMTURU);
                }
                
                if (currentBlock.data != transactionData)
                {
                    return false;
                }
                if (currentBlock.hash != currentBlock.calculateHash())
                {
                    return false;
                }
                if (currentBlock.prevHash != prevBlock.hash)
                {
                    return false;
                }
            }
            return true;
        }
    }
}